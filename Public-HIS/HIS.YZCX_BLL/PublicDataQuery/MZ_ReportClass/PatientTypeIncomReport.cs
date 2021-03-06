﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HIS.YZCX_BLL
{
    /// <summary>
    /// 病人类型收入统计报表
    /// </summary>
    public class PatientTypeIncomReport : BaseReport
    {
        private DataTable _dataResult;
        public override string DataSQL
        {
            get
            {
                StringBuilder sbSQL = new StringBuilder( );
                if ( StatType != StatClassType.收费支付类型 )
                {
                    sbSQL.Append( "select meditype as ID," + base.FieldName + " as code,sum(sub_item_fee) as total_fee " );
                    sbSQL.Append( " from (" + base.BaseSQL + ") a " );
                    sbSQL.Append( " group by meditype," + base.FieldName );
                }
                else
                {
                    sbSQL.Append( "select meditype as ID,'现金' as code, sum(money_fee) as total_fee " );
                    sbSQL.Append( " from (" + base.BaseSQL2 + ") a " );
                    sbSQL.Append( " group by meditype" );
                    sbSQL.Append( " union all " );
                    sbSQL.Append( "select meditype as ID,'POS' as code, sum(pos_fee) as total_fee " );
                    sbSQL.Append( " from (" + base.BaseSQL2 + ") a " );
                    sbSQL.Append( " group by meditype" );
                    sbSQL.Append( " union all " );
                    sbSQL.Append( "select meditype as ID,'医保农合记账' as code, sum(village_fee) as total_fee " );
                    sbSQL.Append( " from (" + base.BaseSQL2 + ") a " );
                    sbSQL.Append( " group by meditype" );
                    sbSQL.Append( " union all " );
                    sbSQL.Append( "select meditype as ID,'单位记账' as code, sum(self_tally) as total_fee " );
                    sbSQL.Append( " from (" + base.BaseSQL2 + ") a " );
                    sbSQL.Append( " group by meditype" );
                    sbSQL.Append( " union all " );
                    sbSQL.Append( "select meditype as ID,'优惠金额' as code, sum(favor_fee) as total_fee " );
                    sbSQL.Append( " from (" + base.BaseSQL2 + ") a " );
                    sbSQL.Append( " group by meditype" );
                }
                return sbSQL.ToString( );
            }
        }
        public override System.Data.DataTable DataResult
        {
            get
            {
                return _dataResult;
            }
            set
            {
                _dataResult = value;
            }
        }
    }
}
