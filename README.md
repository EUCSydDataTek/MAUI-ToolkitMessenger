# ToolkitMessenger

Ref.: [Messenger](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/messenger)

[A Better Pub/Sub Messenger for .NET Applications](https://www.youtube.com/watch?v=vD17OetzGXc)

[Messaging Made Easy: A Guide to WeakReferenceMessenger in .NET MAUI](https://cedricgabrang.medium.com/messaging-made-easy-a-guide-to-weakreferencemessenger-in-net-maui-dc0d131163c)


#### Message
I folderen Messages er defineret en `DeleteItemMessage`:

```csharp
public class DeleteItemMessage : ValueChangedMessage<string>
{
    public DeleteItemMessage(string value) : base(value)
    {
    }
}
```

#### Send

N�r *Delete* funktionen aktiveres i `DetailViewModel`, udsendes en `DeleteItemMessage`:

```csharp
[RelayCommand]
async Task Delete()
{
    WeakReferenceMessenger.Default.Send(new DeleteItemMessage(Text));
    await GoBack();
}
```

#### Receive
I MainViewModel i constructoren abonneres p� DeleteItemMessage message-klassen:

```csharp
WeakReferenceMessenger.Default.Register<DeleteItemMessage>(this, (r, m) =>
{
     Delete(m.Value);
});
```

I tilf�lde af at der modtages en `DeleteItemMessage` vil metoden `Delete()` blive kaldt og vil slette
det aktuelle item. P� denne m�de synkroniseres listen af items p� forsiden, selv om der
slettes et item i `DetailviewModel`.
