namespace MutationExample;

public class Service(IDependentService service)
{
    public bool DoSomething(Guid id)
    {
        var guidHash = id.GetHashCode();

        var value = guidHash%2 == 0 ? "even" : "odd";

        return service.Validate(value);
    }
}