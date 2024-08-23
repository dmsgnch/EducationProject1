using System.Runtime.Serialization;
using System.Security.Permissions;

namespace EducationProject1.Components.Exceptions;

[Serializable]
public sealed class Exception<TExceptionArgs> : Exception, ISerializable
    where TExceptionArgs : ExceptionArgs
{
    private const string KeyForSavingAndRestoring = "Args";
    public TExceptionArgs? Args { get; init; }
    
    public Exception(String? message = null, Exception? innerException = null)
        : this(null, message, innerException)
    { }

    public Exception(TExceptionArgs args, String? message = null,
        Exception? innerException = null) : base(message, innerException)
    {
        Args = args;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    private Exception(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Args = (TExceptionArgs)info.GetValue(KeyForSavingAndRestoring, typeof(TExceptionArgs));
    }
    
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(KeyForSavingAndRestoring, Args);
        base.GetObjectData(info, context);
    }

    public override String Message => (Args is null) ? base.Message : base.Message + " (" + Args.Message + ")";
}