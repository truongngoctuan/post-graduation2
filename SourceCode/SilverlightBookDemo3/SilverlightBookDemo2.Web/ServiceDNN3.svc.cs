using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;

namespace SilverlightBookDemo2.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceDNN3
    {
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        [OperationContract]
        public List<dnn_NewNews_Article> GetAllArticles()
        {
            DNNDataContext context = new DNNDataContext(@"Data Source=HUYNHNGUYEN-PC\SQLEXPRESS;Initial Catalog=DNN6;Integrated Security=True");
            var result = from emp in context.dnn_NewNews_Articles
                         
                         select emp;
            return result.ToList();
        }

        [OperationContract]
        List<EMP_BASIC_INFO> GetAllEmps()
        {
            DataClassesEMPDataContext context = new DataClassesEMPDataContext();
            var result = from emp in context.EMP_BASIC_INFOs
                         select emp;

            return result.ToList();
        }

        [OperationContract]
        List<Article> Get_AllArticles()
        {
            DataClassesEMPDataContext context = new DataClassesEMPDataContext();
            var result = from emp in context.Articles
                         select emp;

            return result.ToList();
        } 
     
    }
}
