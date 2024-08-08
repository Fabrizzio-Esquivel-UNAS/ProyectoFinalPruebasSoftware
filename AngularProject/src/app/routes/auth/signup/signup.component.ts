import { Component, OnInit } from '@angular/core';
import { inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { TranslocoModule, TranslocoService } from '@jsverse/transloco';
import { AuthService } from '@services/auth.service';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { RippleModule } from 'primeng/ripple';
import { catchError, throwError } from 'rxjs';
import { InputNumberModule } from 'primeng/inputnumber';
import { FileUploadEvent, FileUploadModule, UploadEvent } from 'primeng/fileupload';
import { AutoCompleteCompleteEvent, AutoCompleteModule, AutoCompleteSelectEvent } from 'primeng/autocomplete';
import { IEscuelaInfo } from '@domain/escuela.model';
import { UsuarioService } from '@services/usuario.service';
import { DropdownModule } from 'primeng/dropdown';
import { UserForm } from '@ui/user.form';
import { AuthState } from '@state/auth.state';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    standalone: true,
    imports: [
        UserForm,
        TranslocoModule,
        ReactiveFormsModule,
        RouterModule,
        CheckboxModule,
        InputTextModule,
        InputNumberModule,
        DropdownModule,
        AutoCompleteModule,
        StyleClassModule,
        InputMaskModule,
        ButtonModule,
        FileUploadModule,
        RippleModule
    ]
})

export class SignupComponent {
    authService = inject(AuthService);
    usuarioService = inject(UsuarioService);
    translocoService = inject(TranslocoService);
    router = inject(Router);
    authState = inject(AuthState);

    public onSubmit(_result : any) {
        console.log(_result);
        this.router.navigateByUrl(
            this.authState.redirectUrl || '/'
        );
    }
}
