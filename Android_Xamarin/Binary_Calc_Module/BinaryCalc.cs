using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 
using Android.App;'D:\Programmierung\Projekte\ITler-Ein-mal-Eins\Client';
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Android_Xamarin.Binary_Calc_Module
{
    public class BinaryCalc : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            //SetContentView(Resource.Layout.binaryCalculator.axml);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}