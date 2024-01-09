public interface IMission
{
     IMissionConfiguration Configuration { get; }

     bool IsCompleted();
     
     TReturn Accept<TReturn>(IMissionVisitor<TReturn> visitor);
}