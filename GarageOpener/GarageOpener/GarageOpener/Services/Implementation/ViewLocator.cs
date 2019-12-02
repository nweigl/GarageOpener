using System;
using System.Collections.Generic;
using Autofac;
using GarageOpener.ViewModels;
using Xamarin.Forms;

namespace GarageOpener.Services.Implementation
{
    public class ViewLocator : IViewLocator
    {
        private readonly IDictionary<Type, Type> _mapping = new Dictionary<Type, Type>();
        private readonly IComponentContext _componentContext;

        public ViewLocator(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void Register<TViewModel, TView>()
            where TViewModel : BaseViewModel
            where TView : Page
        {
            _mapping[typeof(TViewModel)] = typeof(TView);
        }

        public Page Resolve<TViewModel>()
            where TViewModel : BaseViewModel
        {
            TViewModel viewModel = _componentContext.Resolve<TViewModel>();
            var viewType = _mapping[typeof(TViewModel)];
            var view = _componentContext.Resolve(viewType) as Page;
            view.BindingContext = viewModel;

            return view;
        }
    }
}
