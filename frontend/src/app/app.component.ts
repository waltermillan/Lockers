import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { GLOBAL } from '@configuration/configuration.global';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = '';
  userName:string | null = '';
  userRole:string | null = '';
  isAdmin: boolean = false;

  constructor(public authService: AuthService,
             private router: Router) 
  {
    this.title = GLOBAL.appName;
  }

  logout() {
    this.authService.logout();  // Close session
  }

  ngOnInit(): void {
    this.authService.userName$.subscribe((name) => {
      this.userName = name;
    })  
    this.authService.userRole$.subscribe((role) => {
      this.userRole = role;
      if (role) {
        this.isAdmin = role.toLowerCase() === GLOBAL.adminRole.toLowerCase();
      }
    })  

  }

  getHome(){
    this.router.navigate(['/home']);
  }

  openHelp(){
    window.open('assets/docs/UserManual.pdf');
  }
}
