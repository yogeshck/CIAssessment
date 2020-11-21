using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;

namespace CIAssessment.Helpers
{
    /// <summary>
    /// To raise property change event when new values are set during runtime
    /// </summary>
    public abstract class PropertyChangeHelper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Methods

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var propertyName = GetMemberInfo(property).Name;
            OnPropertyRaised(propertyName);
        }
        #endregion

        #region Private Methods

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private MemberInfo GetMemberInfo(System.Linq.Expressions.Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }
        #endregion
    }
}
