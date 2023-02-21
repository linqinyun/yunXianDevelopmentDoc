namespace mqttServiceDemo.Models
{
    public class Uplink
    {
        // {"Da":1,"En":"gating","Oa":1,"Eo":"status","Nv":"0"}
        /// <summary>
        /// 设备485地址
        /// </summary>
        public int Da { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string En { get; set; }
        /// <summary>
        /// 设备二级设备地址
        /// </summary>
        public int Oa { get; set; }
        /// <summary>
        /// 指令
        /// </summary>
        public string Eo { get; set; }
        /// <summary>
        /// 写入数值
        /// </summary>
        public string Nv { get; set; }
    }
}
