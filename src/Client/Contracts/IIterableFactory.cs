namespace Armut.Iterable.Client.Contracts
{
    public interface IIterableFactory
    {
        UserClient CreateUserClient();

        CommerceClient CreateCommerceClient();

        ListClient CreateListClient();

        EventClient CreateEventClient();
    }
}