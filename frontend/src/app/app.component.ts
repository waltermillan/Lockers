import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { GLOBAL_CONFIG } from './config/config.global';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = '';
  
  constructor(public authService: AuthService,
             private router: Router) 
  {
    this.title = GLOBAL_CONFIG.appName;
  }

  logout() {
    this.authService.logout();  // Close session
  }

  getHome(){
    this.router.navigate(['/home']);
  }

  openHelp(){
    window.open('assets/docs/UserManual.pdf');
  }
}
