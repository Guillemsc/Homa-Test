public interface IMission
{
     IMissionConfiguration Configuration { get; }

     bool IsCompleted();
     string GetDisplayProgress();
     
     TReturn Accept<TReturn>(IMissionVisitor<TReturn> visitor);
}