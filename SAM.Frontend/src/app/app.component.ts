// app.component.ts

import { Component, OnInit } from '@angular/core';
import { AppService } from './app.service';
import { HttpClient } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
