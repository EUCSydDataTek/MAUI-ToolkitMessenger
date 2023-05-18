﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MessengerDemo.Messages;
using System.Collections.ObjectModel;

namespace MessengerDemo.ViewModel;

public partial class MainViewModel : ObservableObject//, IRecipient<DeleteItemMessage>
{
    IConnectivity connectivity;
    public MainViewModel(IConnectivity connectivity)
    {
        Items = new ObservableCollection<string>();
        this.connectivity = connectivity;

        WeakReferenceMessenger.Default.Register<DeleteItemMessage>(this, (r, m) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Delete(m.Value);
            });
        });

        //WeakReferenceMessenger.Default.Register<DeleteItemMessage>(this);
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    string text;

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
            return;
        }

        Items.Add(Text);
        // add our item
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
