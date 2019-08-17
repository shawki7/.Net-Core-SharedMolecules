using System;
using System.Collections.Generic;
using System.Text;

namespace SharedMolecules.Core.OperationResult
{
    public class SMOperationResult<TResult>
       where TResult : struct
    {
        #region Properties
        public List<string> Errors { get; set; }
        private int DefaultResult { get; set; } = 0;
        public TResult Result { get; set; }
        #endregion

        #region Constructor
        public SMOperationResult(TResult result)
        {
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
