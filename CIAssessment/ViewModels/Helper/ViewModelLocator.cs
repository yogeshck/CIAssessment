using Autofac;
using CIAssessment.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CIAssessment.ViewModels.Helper
{
    public class ViewModelLocator
    {
        private static IContainer _container;

        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, new PropertyChangedCallback(OnAutoWireViewModelChanged)));

        public static bool GetAutoWireViewModel(DependencyObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // View models
            builder.RegisterType<MainWindowViewModel>();
            // Services
            builder.RegisterType<RepositoryService>().As<IRepositoryService>().SingleInstance();

            if (_container != null)
            {
                _container.Dispose();
            }
            try
            {
                _container = builder.Build();
                var viewModelType = Type.GetType("CIAssessment.ViewModels.MainWindowViewModel");
                var viewModel = _container.Resolve(viewModelType);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(DependencyObject bindable, DependencyPropertyChangedEventArgs e)
        {
            var view = bindable as FrameworkElement;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            object viewModel = null;
            try
            {
                viewModel = _container.Resolve(viewModelType);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Error in ViewModelLocator - OnAutoWireViewModelChanged");
                sb.AppendLine(ex.Message);

                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    sb.AppendLine(innerException.Message);
                    innerException = innerException.InnerException;
                }

                string error = sb.ToString();
            }

            view.DataContext = viewModel;
        }
    }
}
}
