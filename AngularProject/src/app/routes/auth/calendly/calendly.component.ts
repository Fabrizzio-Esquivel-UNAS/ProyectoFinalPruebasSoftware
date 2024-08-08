import { Component, OnInit } from '@angular/core';
import { inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TranslocoModule } from '@jsverse/transloco';
import { AuthService } from '@services/auth.service';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { catchError, of, throwError } from 'rxjs';
import { AuthState } from '@state/auth.state';
import { CalendarioService } from '@services/calendario.service';
import { ICalendarioInfo, NewCalendarioInfo } from '@domain/calendario.model';

@Component({
    selector: 'app-calendly',
    templateUrl: './calendly.component.html',
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


export class CalendlyComponent implements OnInit {
    authService = inject(AuthService);
    authState = inject(AuthState);
    calendarioService = inject(CalendarioService);
    router = inject(Router);
    route = inject(ActivatedRoute);

    ngOnInit(): void {
        const accessToken = this.route.snapshot.queryParamMap.get('accessToken');
        const refreshToken = this.route.snapshot.queryParamMap.get('refreshToken');
        const expiresIn = this.route.snapshot.queryParamMap.get('expiresIn');
        console.log("AccessToken: ", accessToken);
        console.log("RefreshToken: ", refreshToken);
        console.log("AccessTokenExpiration: ", expiresIn);

        this.calendarioService.add({
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            AccessTokenExpiration: expiresIn
        }).pipe(
            catchError(errorResponse => {
                const detailedErrors = (errorResponse.error as any).detailedErrors;
                if (detailedErrors && detailedErrors[0].code == "CALENDARIO_ALREADY_EXISTS") {
                    console.warn("Ya existe un Calendario autorizado para este Asesor.");
                    return of(errorResponse);
                }
                return throwError(() => errorResponse);
            })            
        ).subscribe(result => {
            this.router.navigateByUrl('calendarioAsesor')
        });
    }
}
