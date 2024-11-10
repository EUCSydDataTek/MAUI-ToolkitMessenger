using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MessengerDemo.Messages;
using MessengerDemo.Views;
using System.Collections.ObjectModel;

namespace MessengerDemo.ViewModel;

public partial class MainViewModel : ObservableObject //, IRecipient<DeleteItemMessage>
{
    public MainViewModel()
    {
        Items = new ObservableCollection<string>();

        // Handle the message here, with r being the recipient and m being the input message. 
        WeakReferenceMessenger.Default.Register<DeleteItemMessage>(this, (r, m) => OnDeleteMessageReceived(m.Value));

        //WeakReferenceMessenger.Default.Register(this);    // Nødvendig for at implementere IRecipient<T>
    }

    // Nødvendig metode for at implementere IRecipient<T>
    public void Receive(DeleteItemMessage message) => OnDeleteMessageReceived(message.Value);

    [RelayCommand]
    void OnDeleteMessageReceived(string s)
    {
        if (Items.Contains(s))
        {
            Items.Remove(s);
        }
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    string text;

    [RelayCommand]
    void Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        Items.Add(Text);
        Text = string.Empty;
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
}
