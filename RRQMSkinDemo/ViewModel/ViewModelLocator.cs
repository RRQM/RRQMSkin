using RRQMSkin.MVVM;

namespace RRQMSkinDemo.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<UserChatViewModel>();
            Instance = this;
          }
        public static ViewModelLocator Instance { get; private set; }

        public MainViewModel MainViewModel { get { return SimpleIoc.Default.GetViewModelInstance<MainViewModel>(); } }
        public UserChatViewModel UserChatViewModel { get { return SimpleIoc.Default.GetViewModelInstance<UserChatViewModel>(); } }
    }
}
