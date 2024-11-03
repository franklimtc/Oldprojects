using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hightcharts
{
    public class Charts
    {
        /// <summary>
        /// Lista de gráficos disponíveis
        /// </summary>
        public enum types { bar, column, line };

        public static string returnChart(string divContainer, string typeChart, string title, string XAxis, string YAxisTitle, string Series, string subtitle = null, string dataLabels = "false")
        {
            return @"$(function () { 
    $('#" + divContainer + @"').highcharts({
        chart: {
            type: '" + typeChart + @"'
        },
        title: {
            text: '" + title + @"'
        },
        subtitle: {
            text: '" + subtitle + @"'
        },
        xAxis: {
            categories: [" + XAxis + @"]
        },
        yAxis: {
            min: 0,
            title: {
                text: '" + YAxisTitle + @"'
            },
            labels: {
                overflow: 'justify'
            }
        },
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: " + dataLabels + @"
                }
            }, 
            bar: {
                dataLabels: {
                    enabled: " + dataLabels + @"
                }
            },
            line: {
                dataLabels: {
                    enabled: " + dataLabels + @"
                }
            }
        },
        series: [" + Series + @"]
    });
});";

        }
        public static string basicLineExample()
        {
            return @"$(function () { 
    $('#container').highcharts({
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Fruit Consumption'
        },
        xAxis: {
            categories: ['Apples', 'Bananas', 'Oranges']
        },
        yAxis: {
            title: {
                text: 'Fruit eaten'
            }
        },
        series: [{
            name: 'Jane',
            data: [1, 0, 4]
        }, {
            name: 'John',
            data: [5, 7, 3]
        }]
    });
});";

        }
    }
}