import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  showMessage: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['admin', [Validators.required]],
      password: ['1234', [Validators.required]],
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;

      this.authService.login(username, password).subscribe({
        next: (response) => {
          if (response.authenticated) {
            this.authService.isLoggedIn();
            this.router.navigate(['/home']);
          } else {
            this.showMessage = true;
            this.errorMessage = '';
          }
        },
        error: (error) => {
          this.showMessage = true;

          if (error.status === 500)
            this.errorMessage =
              'An error occurred with the API connection.\nPlease verify & try again.';
          else if (error.status === 401)
            this.errorMessage = 'Invalid username or password (Unauthorized)';
          else this.errorMessage = 'An error occurred. Please try again.';
        },
      });
    }
  }
}
