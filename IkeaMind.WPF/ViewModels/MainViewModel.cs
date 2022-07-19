using IkeaMind.WPF.Enums;
using IkeaMind.WPF.Stores;
using IkeaMind.WPF.ViewModels.Base;
using IkeaMind.WPF.ViewModels.Controls;
using IkeaMind.WPF.ViewModels.Screens.HomeScreenViewModels;
using IkeaMind.WPF.ViewModels.Screens.NameFirstScreenViewModels;
using IkeaMind.WPF.ViewModels.Screens.PictureFirstScreenViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace IkeaMind.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;
        private readonly ObservableCollection<SideMenuElementViewModel> menuElements;
        private SideMenuElementViewModel selectedMenuElement;

        private readonly HomeScreenViewModel homeScreen;
        private readonly NameFirstScreenViewModel nameFirstScreen;
        private readonly PictureFirstScreenViewModel pictureFirstScreen;

        public IEnumerable<SideMenuElementViewModel> MenuElements => menuElements;
        public SideMenuElementViewModel SelectedMenuElement
        {
            get { return selectedMenuElement; }
            set
            {
                selectedMenuElement = value;
                switch (value.Screen)
                {
                    case MenuElemetsEnum.HomeScreen:
                        navigationStore.CurrentViewModel = homeScreen;
                        break;
                    case MenuElemetsEnum.NameFirstScreen:
                        nameFirstScreen.AppendInitialElementsToObservableCollection();
                        navigationStore.CurrentViewModel = nameFirstScreen;
                        break;
                    case MenuElemetsEnum.PictureFirstScreen:
                        pictureFirstScreen.AppendInitialElementsToObservableCollection();
                        navigationStore.CurrentViewModel = pictureFirstScreen;
                        break;
                    default:
                        navigationStore.CurrentViewModel = homeScreen;
                        break;
                }


                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;
        public MainViewModel(NavigationStore navStore, Action<EventWaitHandle> onInternetFailed)
        {
            menuElements = new ObservableCollection<SideMenuElementViewModel>
            {
                new SideMenuElementViewModel("./../../../Images/Icons/home.png", MenuElemetsEnum.HomeScreen),
                new SideMenuElementViewModel("./../../../Images/Icons/name.png", MenuElemetsEnum.NameFirstScreen),
                new SideMenuElementViewModel("./../../../Images/Icons/chair.png", MenuElemetsEnum.PictureFirstScreen)
            };

            navigationStore = navStore;
            homeScreen = HomeScreenViewModel.GetInstance();
            nameFirstScreen = NameFirstScreenViewModel.CreateInstance(onInternetFailed);
            pictureFirstScreen = PictureFirstScreenViewModel.CreateInstance(onInternetFailed);
        }
    }
}
