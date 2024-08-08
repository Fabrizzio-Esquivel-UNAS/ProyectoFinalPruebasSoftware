import { Component } from '@angular/core';
import { inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { TranslocoModule } from '@jsverse/transloco';
import { AuthService } from '@services/auth.service';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { catchError, throwError } from 'rxjs';
import { AuthState } from '@state/auth.state';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    standalone: true,
    imports: [
        TranslocoModule,
        ReactiveFormsModule,
        RouterModule,
        CheckboxModule,
        InputTextModule,
        ButtonModule,
        RippleModule
    ]
})

export class LoginComponent {
    authService = inject(AuthService);
    authState = inject(AuthState)
    router = inject(Router);

    protected loginForm = new FormGroup({
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required])
    })

    onSubmit() {
        if (!this.loginForm.valid) return
        this.authService
            .login(this.loginForm.value)
            .subscribe({
                next: (result) => {
                    // redirect to dashboard
                    this.router.navigateByUrl(
                        this.authState.redirectUrl || '/'
                    );
                },
            });
    }
}
