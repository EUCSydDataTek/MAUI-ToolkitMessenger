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

        WeakReferenceMessenger.Default.Register<DeleteItemMessage>(this, (r, m) =>
        {
            // Handle the message here, with r being the recipient and m being the
            // input message. Using the recipient passed as input makes it so that
            // the lambda expression doesn't capture "this", improving performance.

            Delete(m.Value);
        });

        //WeakReferenceMessenger.Default.Register(this);
    }

    // Nødvendig metode for at implementere IRecipient<T>
    public void Receive(DeleteItemMessage message)
    {
        Delete(message.Value);
    }

    [RelayCommand]
    void Delete(string s)
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
