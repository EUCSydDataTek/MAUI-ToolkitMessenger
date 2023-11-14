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
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Delete(m.Value);
            });
        });

        //WeakReferenceMessenger.Default.Register(this);
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
    void Delete(string s)
    {
        if (Items.Contains(s))
        {
            Items.Remove(s);
        }
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }

    public void Receive(DeleteItemMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Delete(message.Value);
        });
    }
}
