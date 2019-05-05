import { Component, OnInit, Inject } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { InternalNgModuleRef } from '@angular/core/src/linker/ng_module_factory';


@Component({
  selector: 'app-over-view-data-component',
  templateUrl: './over-view-data-component.component.html',
  styleUrls: ['./over-view-data-component.component.css']
})
export class OverViewDataComponentComponent implements OnInit {

private messages:Message[];

  constructor(private http:HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.crawlData();
   }

  ngOnInit() {
  }

  private crawlData(){
    this.http.get<Message[]>(this.baseUrl + 'api/SampleData/AllMessages').subscribe((data) => {
      this.messages=data;
    })
  }

}
export class Message {
  public primaryKey: number;
  public deviceid: string;
  public datetime: Date;
  public dust:number;
  public ldr:number;
  public co2:number;
  public humidity:number;
  public temp:number;
  public noise:number;
}
