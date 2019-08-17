using System;
using System.Collections.Generic;
using System.Text;

namespace SharedMolecules.Core.OperationResult
{
    public class SMOperationResult<TEntity, TResult>
           where TEntity : class
           where TResult : struct
    {
        #region Properties
        public List<string> Errors { get; set; }
        private int DefaultResult { get; set; } = 0;
        public TEntity Entity { get; set; }
        public TResult Result { get; set; }
        #endregion

        #region Constructor
        public SMOperationResult(TEntity entity, TResult result)
        {
            Entity = entity;
            Result = result;
        }
        #endregion

        #region Methods
        public void addError(string error)
        {
            Errors.Add(error);
        }
        #endregion
    }
}
