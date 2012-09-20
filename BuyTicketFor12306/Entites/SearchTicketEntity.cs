using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuyTicketFor12306.Entites
{
    [Serializable]
    class SearchTicketEntity
    {
        private string _includeStudent = "00";
        /// <summary>
        /// 是否包含学生票
        /// </summary>
        public string IncludeStudent
        {
            get { return _includeStudent; }
            set { _includeStudent = value; }
        }
        private string _method = "queryLeftTicket";

        private string _from_station_telecode = "BJP";// string.Empty;

        public string From_station_telecode
        {
            get { return _from_station_telecode; }
            set { _from_station_telecode = value; }
        }
        private string _start_time_str = "00:00--24:00";

        public string Start_time_str
        {
            get { return _start_time_str; }
            set { _start_time_str = value; }
        }
        private string _to_station_telecode = "THL";// string.Empty;

        public string To_station_telecode
        {
            get { return _to_station_telecode; }
            set { _to_station_telecode = value; }
        }
        private DateTime _train_date = new DateTime(2012, 9, 25);

        public DateTime Train_date
        {
            get { return _train_date; }
            set { _train_date = value; }
        }
        private string _train_no = string.Empty;

        public string Train_no
        {
            get { return _train_no; }
            set { _train_no = value; }
        }
        private string _seatTypeAndNum = string.Empty;

        public string SeatTypeAndNum
        {
            get { return _seatTypeAndNum; }
            set { _seatTypeAndNum = value; }
        }
        private string _trainClass = "QB#D#Z#T#K#QT";

        public string TrainClass
        {
            get { return _trainClass; }
            set { _trainClass = value; }
        }
        private string _trainPassType = "QB";

        public string TrainPassType
        {
            get { return _trainPassType; }
            set { _trainPassType = value; }
        }

        public override string ToString()
        {
            return string.Format("includeStudent={0}&method={1}&orderRequest.from_station_telecode={2}&orderRequest.start_time_str={3}" +
            "&orderRequest.to_station_telecode={4}&orderRequest.train_date={5}&orderRequest.train_no={6}&seatTypeAndNum={7}&trainClass={8}&trainPassType={9}",
                IncludeStudent, _method, From_station_telecode, Start_time_str, To_station_telecode, Train_date.ToString("yyyy-MM-dd"), Train_no, SeatTypeAndNum, TrainClass, TrainPassType);
        }
    }
}
