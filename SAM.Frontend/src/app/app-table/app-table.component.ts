// app.component.ts

import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { AppService } from '../app.service';

@Component({
  selector: 'app-table',
  templateUrl: './app-table.component.html',
  styleUrls: ['./app-table.component.scss'],
})
export class AppTable implements OnInit {
  topApps: any[] = [];
  isLoading: boolean = false;
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  @Input() paid: boolean = false;

  displayedColumns: string[] = [
    'index',
    'name',
    'rating',
    'downloadsString',
    // 'description',
    'category',
  ];

  constructor(private appService: AppService, private http: HttpClient) {}
  nr: number = 5; 

  increment() {
    this.nr++;
    this.ngOnInit();
  }

  decrement() {
    this.nr--;
    this.ngOnInit();
  }
  ngOnInit(): void {
    this.isLoading = true;
    if (!this.paid) {
      this.appService.getTopFreeApps(this.nr).subscribe(
        (data) => {
          this.topApps = data;
          this.dataSource = new MatTableDataSource(this.topApps);
          this.isLoading = false;
        },
        (error) => {
          console.error('Error fetching data:', error);
          this.isLoading = false;
        }
      );
    } else {
      this.appService.getTopPaidApps(this.nr).subscribe(
        (data) => {
          this.topApps = data;
          this.dataSource = new MatTableDataSource(this.topApps);
          this.isLoading = false;
        },
        (error) => {
          console.error('Error fetching data:', error);
          this.isLoading = false;
        }
      );
    }
  }
}
