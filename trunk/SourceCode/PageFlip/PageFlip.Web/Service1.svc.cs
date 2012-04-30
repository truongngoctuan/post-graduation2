using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;

namespace PageFlip.Web
{
    [ServiceContract(Namespace = "")]
    [SilverlightFaultBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1
    {
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]
        [OperationContract]
        List<EMP_BASIC_INFO> GetAllEmps()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            var result = from emp in context.EMP_BASIC_INFOs
                         select emp;

            return result.ToList();
        }

        [OperationContract]
        List<Article> Get_AllArticles()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            var result = from emp in context.Articles
                         select emp;

            return result.ToList();
        } 
    }
}
