using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MessengerDemo.Messages;

public class DeleteItemMessage : ValueChangedMessage<string>
{
    public DeleteItemMessage(string value) : base(value)
    {
    }
}
