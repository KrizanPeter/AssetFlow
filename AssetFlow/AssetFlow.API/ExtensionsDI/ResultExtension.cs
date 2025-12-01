using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentResults;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace AssetFlow.API.ExtensionsDI
{
    public static class ResultExtensions
    {
        public static IResult ToApiResult<T>(this Result<T> result)
        {
            if (result == null)
                return Results.Problem("Result is null.");

            if (result.IsSuccess)
                return Results.Ok(result.Value);
            var errors = result.Errors.Select(e => new { e.Message, e.Metadata }).ToList();
            var error = result.Errors.FirstOrDefault();
            if (error == null)
                return Results.Problem("An unknown error occurred.");

            // Determine the runtime type name to avoid compile-time dependency on specific error classes.
            var typeName = error.GetType().Name;

            if (string.Equals(typeName, "ValidationError", StringComparison.OrdinalIgnoreCase))
            {
                // Try to extract a property name if present (handle several possible shapes via reflection).
                string? property = null;

                // 1) Look for a direct property named "Property" or "PropertyName".
                var propInfo = error.GetType().GetProperty("Property") ?? error.GetType().GetProperty("PropertyName");
                if (propInfo != null)
                {
                    property = propInfo.GetValue(error)?.ToString();
                }
                else
                {
                    // 2) Look for a Metadata property that may be a dictionary.
                    var metaProp = error.GetType().GetProperty("Metadata");
                    if (metaProp != null)
                    {
                        var metaVal = metaProp.GetValue(error);
                        // If it's a generic IDictionary<string, object>
                        if (metaVal is IDictionary<string, object> dict)
                        {
                            if (dict.TryGetValue("Property", out var v) || dict.TryGetValue("PropertyName", out v))
                                property = v?.ToString();
                        }
                        else if (metaVal is IDictionary nonGenDict) // non-generic IDictionary
                        {
                            if (nonGenDict.Contains("Property"))
                                property = nonGenDict["Property"]?.ToString();
                            else if (nonGenDict.Contains("PropertyName"))
                                property = nonGenDict["PropertyName"]?.ToString();
                        }
                        // else: unknown metadata shape — ignore gracefully.
                    }
                }

                // Return BadRequest with Message and optional Property key (nulls are fine in anonymous objects).
                return Results.BadRequest(new { error.Message, Property = property });
            }

            if (string.Equals(typeName, "NotFoundError", StringComparison.OrdinalIgnoreCase))
            {
                return Results.NotFound(new { error.Message });
            }

            if (string.Equals(typeName, "BusinessError", StringComparison.OrdinalIgnoreCase))
            {
                return Results.UnprocessableEntity(new { error.Message });
            }

            // Fallback: aggregate messages into a single string because Results.Problem expects a string detail.
            var aggregated = string.Join("; ", result.Errors.Select(e => e.Message));
            return Results.Problem(aggregated);
        }
    }
}
