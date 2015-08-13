using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ards_v6_Tester
{
    public partial class Form1 : Form
    {
        public static Form1 Instance { get; set; }
        delegate void TextBoxDelegate(string message);

        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            Instance = this;

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = (1000) * (2);
            timer.Enabled = true;

            rb_AddReqServer.Checked = true;
            rb_AddReqMeta.Checked = true;
            rb_AddResource.Checked = true;
            rb_AddRequest.Checked = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            pb_ReqSer.Visible = false;
            pb_ReqMeta.Visible = false;
            pb_Res.Visible = false;
            pb_Req.Visible = false;
            timer.Stop();
        }

        public void UpdatingTextBox(string msg)
        {
            if (this.txt_Result.InvokeRequired)
                this.txt_Result.Invoke(new TextBoxDelegate(UpdatingTextBox), new object[] { msg });
            else
                this.txt_Result.AppendText("\r\n\r\n" + JToken.Parse(msg).ToString(Newtonsoft.Json.Formatting.Indented));
        }

        #region RequestServerTab

        private string SetReqSerRequestUrl(string type)
        {
            return string.Format("http://{0}/{1}/{2}", CommonLoader.Instance.ArdsServer, type, txt_ReqSerId.Text);
        }

        private void rb_AddReqServer_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqSerRequestUrl.Text = string.Format("http://{0}/requestserver/add", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Company\":1,\"Tenant\":3,\"Class\":\"CALLSERVER\",\"Type\":\"ARDS\",\"Category\":\"CALL\",\"CallbackUrl\":\"http://localhost:2228/api/Result\",\"ServerID\":1}";
            txt_ReqSerRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_EditReqServer_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqSerRequestUrl.Text = string.Format("http://{0}/requestserver/set", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Company\":1,\"Tenant\":3,\"Class\":\"CALLSERVER\",\"Type\":\"ARDS\",\"Category\":\"CALL\",\"CallbackUrl\":\"http://localhost:2228/callback/print\",\"ServerID\":1}";
            txt_ReqSerRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_ViewReqServer_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqSerRequestUrl.Text = SetReqSerRequestUrl("requestserver");
            txt_ReqSerRequestBody.Clear();
        }

        private void rb_SearchReqServerByTags_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqSerRequestUrl.Text = string.Format("http://{0}/requestserver/searchbytag", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Tags\":[\"company_1\" , \"tenant_3\", \"class_CALLSERVER\", \"type_ARDS\", \"category_CALL\"]}";
            txt_ReqSerRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_RemoveReqServer_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqSerRequestUrl.Text = SetReqSerRequestUrl("requestserver");
            txt_ReqSerRequestBody.Clear();
        }

        private void txt_ReqSerId_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewReqServer.Checked)
            {
                txt_ReqSerRequestUrl.Text = SetReqSerRequestUrl("requestserver");
            }
            else if (rb_RemoveReqServer.Checked)
            {
                txt_ReqSerRequestUrl.Text = SetReqSerRequestUrl("requestserver");
            }
        }

        private void txt_ReqSerCompany_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewReqServer.Checked)
            {
                txt_ReqSerRequestUrl.Text = SetReqSerRequestUrl("requestserver");
            }
            else if (rb_RemoveReqServer.Checked)
            {
                txt_ReqSerRequestUrl.Text = SetReqSerRequestUrl("requestserver");
            }
        }

        private void txt_ReqSerTenant_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewReqServer.Checked)
            {
                txt_ReqSerRequestUrl.Text = SetReqSerRequestUrl("requestserver");
            }
            else if (rb_RemoveReqServer.Checked)
            {
                txt_ReqSerRequestUrl.Text = SetReqSerRequestUrl("requestserver");
            }
        }

        private void btn_ReqServerDoPost_Click(object sender, EventArgs e)
        {
            var authToken = string.Format("{0}#{1}", txt_ReqSerTenant.Text, txt_ReqSerCompany.Text);
            txt_Result.Text = RestClient.DoPost(txt_ReqSerRequestUrl.Text, txt_ReqSerRequestBody.Text, authToken);
            pb_ReqSer.Visible = true;
            timer.Start();
        }

        private void btn_ReqServerDoGet_Click(object sender, EventArgs e)
        {
            var authToken = string.Format("{0}#{1}", txt_ReqSerTenant.Text, txt_ReqSerCompany.Text);
            txt_Result.Text = RestClient.DoGet(txt_ReqSerRequestUrl.Text, authToken);
            pb_ReqSer.Visible = true;
            timer.Start();
        }

        private void btn_ReqServerDoRemove_Click(object sender, EventArgs e)
        {
            var authToken = string.Format("{0}#{1}", txt_ReqSerTenant.Text, txt_ReqSerCompany.Text);
            txt_Result.Text = RestClient.DoRemove(txt_ReqSerRequestUrl.Text, authToken);
            pb_ReqSer.Visible = true;
            timer.Start();
        }

        #endregion


        #region RequestMetaTab

        private string SetReqMetaRequestUrl(string method, string type)
        {
            return string.Format("http://{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", CommonLoader.Instance.ArdsServer, type, method, txt_ReqMetaCompany.Text, txt_ReqMetaTenant.Text, txt_ReqMetaClass.Text, txt_ReqMetaType.Text, txt_ReqMetaCategory.Text);
        }

        private void rb_AddReqMeta_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqMetaRequestUrl.Text = string.Format("http://{0}/requestmeta/add", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Class\":\"CALLSERVER\",\"Type\":\"ARDS\",\"Category\":\"CALL\",\"ServingAlgo\":\"CALLBACK\",\"AttributeMeta\":[{\"AttributeClass\":\"CALLSERVER\", \"AttributeType\":\"CALL\", \"AttributeCategory\":\"LANGUAGE\", \"AttributeCode\":[\"123456\", \"875463\", \"452632\"], \"WeightPrecentage\":\"100\"}],\"Company\":1,\"Tenant\":3,\"HandlingAlgo\":\"SINGLE\",\"SelectionAlgo\":\"BASIC\",\"MaxReservedTime\":10,\"MaxRejectCount\":1500,\"ReqHandlingAlgo\":\"QUEUE\",\"ReqSelectionAlgo\":\"LONGESTWAITING\"}";
            txt_ReqMetaRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_EditReqMeta_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqMetaRequestUrl.Text = string.Format("http://{0}/requestmeta/set", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Class\":\"CALLSERVER\",\"Type\":\"ARDS\",\"Category\":\"CALL\",\"ServingAlgo\":\"CALLBACK\",\"AttributeMeta\":[{\"AttributeClass\":\"CALLSERVER\", \"AttributeType\":\"CALL\", \"AttributeCategory\":\"LANGUAGE\", \"AttributeCode\":[\"123456\", \"875463\", \"452632\"], \"WeightPrecentage\":\"100\"}],\"Company\":1,\"Tenant\":3,\"HandlingAlgo\":\"SINGLE\",\"SelectionAlgo\":\"BASIC\",\"MaxReservedTime\":10,\"MaxRejectCount\":1500,\"ReqHandlingAlgo\":\"QUEUE\",\"ReqSelectionAlgo\":\"LONGESTWAITING\"}";
            txt_ReqMetaRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_ViewReqMeta_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqMetaRequestUrl.Text = SetReqMetaRequestUrl("get", "requestmeta");
            txt_ReqMetaRequestBody.Clear();
        }

        private void rb_SearchReqMetaByTags_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqMetaRequestUrl.Text = string.Format("http://{0}/requestmeta/searchbytag", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Tags\":[\"company_1\", \"tenant_3\", \"class_CALLSERVER\", \"type_ARDS\", \"category_CALL\"]}";
            txt_ReqMetaRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        
        }

        private void rb_RemoveReqMeta_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqMetaRequestUrl.Text = SetReqMetaRequestUrl("remove", "requestmeta");
            txt_ReqMetaRequestBody.Clear();
        }

        private void txt_ReqMetaClass_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewReqMeta.Checked)
            {
                txt_ReqMetaRequestUrl.Text = SetReqMetaRequestUrl("get", "requestserver");
            }
            else if (rb_RemoveReqMeta.Checked)
            {
                txt_ReqMetaRequestUrl.Text = SetReqMetaRequestUrl("remove", "requestserver");
            }
        }

        private void txt_ReqMetaType_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewReqMeta.Checked)
            {
                txt_ReqMetaRequestUrl.Text = SetReqMetaRequestUrl("get", "requestserver");
            }
            else if (rb_RemoveReqMeta.Checked)
            {
                txt_ReqMetaRequestUrl.Text = SetReqMetaRequestUrl("remove", "requestserver");
            }
        }

        private void txt_ReqMetaCategory_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewReqMeta.Checked)
            {
                txt_ReqMetaRequestUrl.Text = SetReqMetaRequestUrl("get", "requestserver");
            }
            else if (rb_RemoveReqMeta.Checked)
            {
                txt_ReqMetaRequestUrl.Text = SetReqMetaRequestUrl("remove", "requestserver");
            }
        }

        private void btn_ReqMetaDoPost_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoPost(txt_ReqMetaRequestUrl.Text, txt_ReqMetaRequestBody.Text,"1#1");
            pb_ReqMeta.Visible = true;
            timer.Start();
        }

        private void btn_ReqMetaDoGet_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoGet(txt_ReqMetaRequestUrl.Text, "1#1");
            pb_ReqMeta.Visible = true;
            timer.Start();
        }

        private void btn_ReqMetaDoRemove_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoRemove(txt_ReqMetaRequestUrl.Text, "1#1");
            pb_ReqMeta.Visible = true;
            timer.Start();
        }

        #endregion


        #region ResourceTab

        private string _resourceState = "Available";
        private string _resourceCSState = "Available";

        private string SetResRequestUrl(string method, string type)
        {
            return string.Format("http://{0}/{1}/{2}/{3}/{4}/{5}", CommonLoader.Instance.ArdsServer, type, method, txt_ResCompany.Text, txt_ResTenant.Text, txt_ResResourceId.Text);
        }

        private void SetCSStateUpdateInfo()
        {
            txt_ResRequestUrl.Text = string.Format("http://{0}/resource/cs/updatebysessionid", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Company\":" + txt_ResCompany.Text + ", \"Tenant\":" + txt_ResTenant.Text + ", \"ReqClass\":\"CALLSERVER\", \"ReqType\":\"ARDS\", \"ReqCategory\":\"CALL\", \"ResourceId\":\"" + txt_ResResourceId.Text + "\", \"SessionId\":\"" + txt_ResSessionId.Text + "\", \"State\":\"" + _resourceCSState + "\", \"OtherInfo\":\"\"}";
            txt_ResRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);        
        }

        private void SetResourceStateInfo()
        {
            txt_ResRequestUrl.Text = string.Format("http://{0}/resource/state/push", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Company\":" + txt_ResCompany.Text + ", \"Tenant\":" + txt_ResTenant.Text + ", \"ResourceId\":\"" + txt_ResResourceId.Text + "\", \"State\":\"" + _resourceState + "\"}";
            txt_ResRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        
        }

        private void rb_AddResource_CheckedChanged(object sender, EventArgs e)
        {
            txt_ResRequestUrl.Text = string.Format("http://{0}/resource/add", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Company\":1, \"Tenant\":3, \"Class\":\"HUMANRESOURCE\", \"Type\":\"CALLCENTER\", \"Category\":\"AGENT\", \"ResourceId\":\"555555555\", \"ResourceAttributeInfo\":[{\"Attribute\":\"123456\",\"Class\":\"CALLSERVER\",\"Type\":\"CALL\",\"Category\":\"LANGUAGE\",\"Percentage\":90},{\"Attribute\":\"875463\",\"Class\":\"CALLSERVER\",\"Type\":\"CALL\",\"Category\":\"LANGUAGE\",\"Percentage\":70}], \"ConcurrencyInfo\":[{\"Class\":\"CALLSERVER\",\"Type\":\"ARDS\",\"Category\":\"CALL\",\"NoOfSlots\":1}], \"OtherInfo\":\"{\\\"Extention\\\":3562, \\\"DialHostName\\\":\\\"192.168.2.38\\\"}\"}";
            txt_ResRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_EditResource_CheckedChanged(object sender, EventArgs e)
        {
            txt_ResRequestUrl.Text = string.Format("http://{0}/resource/set", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Company\":1, \"Tenant\":3, \"Class\":\"HUMANRESOURCE\", \"Type\":\"CALLCENTER\", \"Category\":\"AGENT\", \"ResourceId\":\"555555555\", \"ResourceAttributeInfo\":[{\"Attribute\":\"123456\",\"Class\":\"CALLSERVER\",\"Type\":\"CALL\",\"Category\":\"LANGUAGE\",\"Percentage\":90},{\"Attribute\":\"875463\",\"Class\":\"CALLSERVER\",\"Type\":\"CALL\",\"Category\":\"LANGUAGE\",\"Percentage\":70}], \"ConcurrencyInfo\":[{\"Class\":\"CALLSERVER\",\"Type\":\"ARDS\",\"Category\":\"CALL\",\"NoOfSlots\":1}], \"OtherInfo\":\"{\\\"Extention\\\":3562, \\\"DialHostName\\\":\\\"192.168.2.38\\\"}\"}";
            txt_ResRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        
        }

        private void rb_ViewResource_CheckedChanged(object sender, EventArgs e)
        {
            txt_ResRequestUrl.Text = SetResRequestUrl("get", "resource");
            txt_ResRequestBody.Clear();
        }

        private void rb_SearchResourceByTags_CheckedChanged(object sender, EventArgs e)
        {
            txt_ResRequestUrl.Text = string.Format("http://{0}/resource/searchbytag", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Tags\":[\"company_1\", \"tenant_3\", \"class_HUMANRESOURCE\", \"type_CALLCENTER\", \"category_AGENT\"]}";
            txt_ResRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_RemoveResource_CheckedChanged(object sender, EventArgs e)
        {
            txt_ResRequestUrl.Text = SetResRequestUrl("remove", "resource");
            txt_ResRequestBody.Clear();
        }

        private void rb_UpdateResourceState_CheckedChanged(object sender, EventArgs e)
        {
            SetResourceStateInfo();
        }

        private void rb_UpdateSlotStateBySessionId_CheckedChanged(object sender, EventArgs e)
        {
            SetCSStateUpdateInfo();  
        }

        private void rb_CSAvailable_CheckedChanged(object sender, EventArgs e)
        {
            _resourceCSState = "Available";
            SetCSStateUpdateInfo();
        }

        private void rb_CSConnected_CheckedChanged(object sender, EventArgs e)
        {
            _resourceCSState = "Connected";
            SetCSStateUpdateInfo();
        }

        private void rb_CSReserved_CheckedChanged(object sender, EventArgs e)
        {
            _resourceCSState = "Reserved";
            SetCSStateUpdateInfo();
        }

        private void rb_ResourceStateNA_CheckedChanged(object sender, EventArgs e)
        {
            _resourceState = "NotAvailable";
            SetResourceStateInfo();
        }

        private void rb_ResourceStateAvailable_CheckedChanged(object sender, EventArgs e)
        {
            _resourceState = "Available";
            SetResourceStateInfo();
        }

        private void rb_ResourceStateReserved_CheckedChanged(object sender, EventArgs e)
        {
            _resourceState = "Reserved";
            SetResourceStateInfo();
        }

        private void txt_ResCompany_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewResource.Checked)
            {
                txt_ResRequestUrl.Text = SetResRequestUrl("get", "resource");
            }
            else if (rb_RemoveResource.Checked)
            {
                txt_ResRequestUrl.Text = SetResRequestUrl("remove", "resource");
            }
            else if (rb_UpdateResourceState.Checked)
            {
                SetResourceStateInfo();
            }
            else if (rb_UpdateSlotStateBySessionId.Checked)
            {
                SetCSStateUpdateInfo();
            }
        }

        private void txt_ResTenant_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewResource.Checked)
            {
                txt_ResRequestUrl.Text = SetResRequestUrl("get", "resource");
            }
            else if (rb_RemoveResource.Checked)
            {
                txt_ResRequestUrl.Text = SetResRequestUrl("remove", "resource");
            }
            else if (rb_UpdateResourceState.Checked)
            {
                SetResourceStateInfo();
            }
            else if (rb_UpdateSlotStateBySessionId.Checked)
            {
                SetCSStateUpdateInfo();
            }
        }

        private void txt_ResResourceId_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewResource.Checked)
            {
                txt_ResRequestUrl.Text = SetResRequestUrl("get", "resource");
            }
            else if (rb_RemoveResource.Checked)
            {
                txt_ResRequestUrl.Text = SetResRequestUrl("remove", "resource");
            }
            else if (rb_UpdateResourceState.Checked)
            {
                SetResourceStateInfo();
            }
            else if (rb_UpdateSlotStateBySessionId.Checked)
            {
                SetCSStateUpdateInfo();
            }
        }

        private void txt_ResSessionId_TextChanged(object sender, EventArgs e)
        {
            SetCSStateUpdateInfo();
        }

        private void btn_ResourceDoPost_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoPost(txt_ResRequestUrl.Text, txt_ResRequestBody.Text, "1#1");
            pb_Res.Visible = true;
            timer.Start();
        }

        private void btn_ResourceDoGet_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoGet(txt_ResRequestUrl.Text, "1#1");
            pb_Res.Visible = true;
            timer.Start();
        }

        private void btn_ResourceDoRemove_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoRemove(txt_ResRequestUrl.Text, "1#1");
            pb_Res.Visible = true;
            timer.Start();
        }

        #endregion

        
        #region RequestTab

        private string SetReqRequestUrl(string method, string type)
        {
            return string.Format("http://{0}/{1}/{2}/{3}/{4}/{5}", CommonLoader.Instance.ArdsServer, type, method, txt_ReqCompany.Text, txt_ReqTenant.Text, txt_ReqRequestId.Text);
        }

        private string SetReqRequestUrl(string method, string type, string reason)
        {
            return string.Format("http://{0}/{1}/{2}/{3}/{4}/{5}/{6}", CommonLoader.Instance.ArdsServer, type, method, txt_ReqCompany.Text, txt_ReqTenant.Text, txt_ReqRequestId.Text, reason);
        }

        private void rb_AddRequest_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqRequestUrl.Text = string.Format("http://{0}/request/add", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Company\":1,\"Tenant\":3,\"Class\":\"CALLSERVER\",\"Type\":\"ARDS\",\"Category\":\"CALL\",\"SessionId\":\"1111\",\"Attributes\":[\"123456\"],\"RequestServerId\":\"1\",\"Priority\":\"L\",\"OtherInfo\":\"{\\\"ResourceCount\\\":2}\"}";
            txt_ReqRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_EditRequest_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqRequestUrl.Text = string.Format("http://{0}/request/set", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Company\":1,\"Tenant\":3,\"Class\":\"CALLSERVER\",\"Type\":\"ARDS\",\"Category\":\"CALL\",\"SessionId\":\"1111\",\"Attributes\":[\"123456\"],\"RequestServerId\":\"1\",\"Priority\":\"L\",\"OtherInfo\":\"\"}";
            txt_ReqRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_ViewRequest_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqRequestUrl.Text = SetReqRequestUrl("get", "request");
            txt_ReqRequestBody.Clear();
        }

        private void rb_SearchRequestByTags_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqRequestUrl.Text = string.Format("http://{0}/request/searchbytag", CommonLoader.Instance.ArdsServer);
            var reqString = "{\"Tags\":[\"company_1\", \"tenant_3\", \"class_CALLSERVER\", \"type_ARDS\", \"category_CALL\", \"reqserverid_1\", \"priority_L\", \"servingalgo_CALLBACK\", \"handlingalgo_SINGLE\", \"selectionalgo_BASIC\"]}";
            txt_ReqRequestBody.Text = JToken.Parse(reqString).ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private void rb_RemoveRequest_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqRequestUrl.Text = SetReqRequestUrl("remove", "request");
            txt_ReqRequestBody.Clear();
        }

        private void rb_RejectRequest_CheckedChanged(object sender, EventArgs e)
        {
            txt_ReqRequestUrl.Text = SetReqRequestUrl("reject", "request", cmb_ReqRejectReason.Text);
            txt_ReqRequestBody.Clear();
        }

        private void txt_ReqCompany_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("get", "request");
            }
            else if (rb_RemoveRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("remove", "request");
            }
            else if (rb_RejectRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("reject", "request", cmb_ReqRejectReason.Text);
            }
        }

        private void txt_ReqTenant_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("get", "request");
            }
            else if (rb_RemoveRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("remove", "request");
            }
            else if (rb_RejectRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("reject", "request", cmb_ReqRejectReason.Text);
            }
        }

        private void txt_ReqRequestId_TextChanged(object sender, EventArgs e)
        {
            if (rb_ViewRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("get", "request");
            }
            else if (rb_RemoveRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("remove", "request");
            }
            else if (rb_RejectRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("reject", "request", cmb_ReqRejectReason.Text);
            }
        }

        private void cmb_ReqRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rb_RejectRequest.Checked)
            {
                txt_ReqRequestUrl.Text = SetReqRequestUrl("reject", "request", cmb_ReqRejectReason.Text);
            }
        }

        private void btn_RequestDoPost_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoPost(txt_ReqRequestUrl.Text, txt_ReqRequestBody.Text, "1#1");
            pb_Req.Visible = true;
            timer.Start();
        }

        private void btn_RequestDoGet_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoGet(txt_ReqRequestUrl.Text, "1#1");
            pb_Req.Visible = true;
            timer.Start();
        }

        private void btn_RequestDoRemove_Click(object sender, EventArgs e)
        {
            txt_Result.Text = RestClient.DoRemove(txt_ReqRequestUrl.Text, "1#1");
            pb_Req.Visible = true;
            timer.Start();
        }

        #endregion
        
    }
}
