﻿using MessengerDemo.ViewModel;

namespace MessengerDemo.Views;

public partial class MainPage : ContentPage
{

	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

