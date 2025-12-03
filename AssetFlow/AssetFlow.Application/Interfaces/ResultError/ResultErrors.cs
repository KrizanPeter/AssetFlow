using FluentResults;

namespace AssetFlow.Application.Errors
{
    // Validation error: input data is invalid
    public class ValidationError : Error
    {
        public string Property { get; }

        private ValidationError(string property, string message) : base(message)
        {
            Property = property;
        }

        private ValidationError(string property, string message, List<Error> reasons) : base(message)
        {
            Property = property;
            Reasons.AddRange(reasons);
        }

        public static ValidationError Of(string property, string message) =>
            new ValidationError(property, message);

        public static ValidationError Of(string property, string message, List<Error> reasons) =>
            new ValidationError(property, message, reasons);
    }

    // Not found error: resource does not exist
    public class NotFoundError : Error
    {
        private NotFoundError(string message) : base(message) { }

        public static NotFoundError Of(string message) =>
            new NotFoundError(message);
    }

    // Business rule error: domain logic violation
    public class BusinessError : Error
    {
        private BusinessError(string message) : base(message) { }

        public static BusinessError Of(string message) =>
            new BusinessError(message);
    }

    // Authorization error: user not allowed
    public class UnauthorizedError : Error
    {
        private UnauthorizedError(string message = "Unauthorized") : base(message) { }

        public static UnauthorizedError Of() =>
            new UnauthorizedError();

        public static UnauthorizedError Of(string message) =>
            new UnauthorizedError(message);
    }

    // Forbidden error: user authenticated but not permitted
    public class ForbiddenError : Error
    {
        private ForbiddenError(string message = "Forbidden") : base(message) { }

        public static ForbiddenError Of() =>
            new ForbiddenError();

        public static ForbiddenError Of(string message) =>
            new ForbiddenError(message);
    }

    // Conflict error: resource state conflict (e.g. duplicate key)
    public class ConflictError : Error
    {
        private ConflictError(string message) : base(message) { }

        public static ConflictError Of(string message) =>
            new ConflictError(message);
    }

    // Internal/system error: unexpected failure
    public class SystemError : Error
    {
        private SystemError(string message) : base(message) { }

        public static SystemError Of(string message) =>
            new SystemError(message);
    }
}

