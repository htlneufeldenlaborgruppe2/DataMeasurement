import { Component, OnInit, Inject } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Color, BaseChartDirective, Label } from 'ng2-charts';
import * as pluginAnnotations from 'chartjs-plugin-annotation';
import { Message } from '../over-view-data-component/over-view-data-component.component'
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-display-graph',
  templateUrl: './display-graph.component.html',
  styleUrls: ['./display-graph.component.css']
})
export class DisplayGraphComponent implements OnInit {

  public amountOfLastMessages: number = 5;
  public optionsSelect: string[];
  public itemSelected:string="null";
  public loadingStateDevices:string="loading";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http.get<string[]>(this.baseUrl + 'api/SampleData/GetDeviceIDs').subscribe((data) => {
      this.optionsSelect = data;
      this.loadingStateDevices="finished"
    }, (err)=>{
      console.log(err);
      this.loadingStateDevices="failed";
    });
  }
  

  ngOnInit() {
  }
  public lineChartData: ChartDataSets[] = [
    { data: [], label: 'Dust' },
    { data: [], label: 'Dust' },
    { data: [], label: 'LDR' },
    { data: [], label: 'CO2' },
    { data: [], label: 'Humidity' },
    { data: [], label: 'Temperature' },
    { data: [], label: 'Noise' },
  ];
  public lineChartLabels: Label[] = [];
  public lineChartOptions: (ChartOptions & { annotation: any }) = {
    responsive: true,
    scales: {
      // We use this empty structure as a placeholder for dynamic theming.
      xAxes: [{}],
      yAxes: [
        {
          id: 'y-axis-0',
          position: 'left',
        },
        {
          id: 'y-axis-1',
          position: 'right',
          gridLines: {
            color: 'rgba(255,0,0,0.3)',
          },
          ticks: {
            fontColor: 'red',
          } 
        }
      ]
    },
    annotation: {
    },
  };
  public lineChartColors: Color[] = [
    { // grey; dust
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // dark grey; ldr
      backgroundColor: 'rgba(200,50,50,0.5)',
      borderColor: 'rgba(200,50,50,1)',
      pointBackgroundColor: 'rgba(77,83,96,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(77,83,96,1)'
    },
    { // red; co2
      backgroundColor: 'rgba(255,0,0,0.3)',
      borderColor: 'red',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // green; humidity
      backgroundColor: 'rgba(0,255,0,0.3)',
      borderColor: 'green',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // blau; temp
      backgroundColor: 'rgba(0,0,255,0.3)',
      borderColor: 'blue',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // türkis; noise
      backgroundColor: 'rgba(102,255,249,0.3)',
      borderColor: 'rgba(102,255,249,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];
  public lineChartLegend = true;
  public lineChartType = 'line';
  public lineChartPlugins = [pluginAnnotations];

  chartHovered($event) {

  }
  chartClicked($event) {

  }
  displayClick() {
    console.log(this.amountOfLastMessages);

    let messages: Message[];
    this.http.get<Message[]>(this.baseUrl + 'api/SampleData/GetLineGraphData?deviceid='+this.itemSelected+'&items=' + this.amountOfLastMessages.toString()).subscribe((data) => {
      messages = data;
      console.log(messages);
      console.log(this.itemSelected);
      this.lineChartData = [
        { data: [], label: 'Dust' },
        { data: [], label: 'LDR',  yAxisID:'y-axis-1'},
        { data: [], label: 'CO2' , yAxisID:'y-axis-1'},
        { data: [], label: 'Humidity' },
        { data: [], label: 'Temperature' },
        { data: [], label: 'Noise' },
      ];
      this.lineChartLabels = [];
      messages.forEach(element => {

        let data: number[] = this.lineChartData[0].data as number[];
        data.push(element.dust);
        data = this.lineChartData[1].data as number[];
        data.push(element.ldr);
        data = this.lineChartData[2].data as number[];
        data.push(element.co2);
        data = this.lineChartData[3].data as number[];
        data.push(element.humidity);
        data = this.lineChartData[4].data as number[];
        data.push(element.temp);
        data = this.lineChartData[5].data as number[];
        data.push(element.noise);

        // this.lineChartData[1].data.push(element.ldr);
        // this.lineChartData[2].data.push(element.co2);
        // this.lineChartData[3].data.push(element.humidity);
        // this.lineChartData[4].data.push(element.temp);
        // this.lineChartData[5].data.push(element.noise);
        this.lineChartLabels.push(element.timesent.toString());
      });
    })
  }
}