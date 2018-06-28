using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ITler_Ein_mal_Eins.Control
{
    class InterfaceIdentifier
    {
        //Erstellen einer Instanz
        PerformanceCounter performanceCPU; 

        public struct Struct_performanceCPU
        {
            public float cpu_total, cpu_1, cpu_2, cpu_3, cpu_4;
            public Struct_performanceCPU (float cpuTotal, float cpu1, float cpu2, float cpu3, float cpu4)
            {
                cpu_total = cpuTotal;
                cpu_1 = cpu1;
                cpu_2 = cpu2;
                cpu_3 = cpu3;
                cpu_4 = cpu4;
            }
        }
        Struct_performanceCPU struct_performanceCPU;

        public Struct_performanceCPU PerformanceControl_CPU() // Initialisieren
        {
            performanceCPU = new PerformanceCounter();
            performanceCPU.CategoryName     = "Processor";
            performanceCPU.CounterName      = "% Processor Time";
            performanceCPU.InstanceName     = "_Total";
            struct_performanceCPU.cpu_total = performanceCPU.NextValue();

            return struct_performanceCPU;
        }



        /*
        Beispiel zur Verwendung
        */

        //private void Main(string[] args)
        //{
        //    PerformanceControl_CPU(); // Initialisieren


        //}

    }
}
