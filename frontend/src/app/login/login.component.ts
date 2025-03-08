import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  username: string = '';
  password: string = '';
  showMessage: boolean = false;

  constructor(public authService: AuthService, 
              private router: Router) { }

  onSubmit(): void {
    this.authService.login(this.username, this.password).subscribe({
      next: (data) => {
        console.log('User autenticated!');
        this.authService.loggedIn = true;
        this.router.navigate(['/home']);
      },
      error: (error) => {
        console.log('User dont autenticated!');
        this.showMessage = true;
      }
    });
  }
}
