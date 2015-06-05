using System;

namespace SlimsteMens.Services.SlimsteAdminService
{
    public partial class SlimsteServiceClient : IDisposable
    {
        public void Dispose()
        {
            var success = false;
            try
            {
                Close();
                success = true;
            }
            catch
            {
                if(!success)
                    Abort();
            }
        }
    }
}
