// home.component.ts
import { Component } from '@angular/core';
import { GLOBAL_CONFIG } from '../config/config.global';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  title:string = '';

  constructor() { 
    this.title = GLOBAL_CONFIG.appName;
  }

}
