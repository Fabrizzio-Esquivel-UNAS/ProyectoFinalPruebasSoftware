import { Component, EventEmitter, input, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
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
import { catchError, take, throwError } from 'rxjs';
import { InputNumberModule } from 'primeng/inputnumber';
import { FileUploadEvent, FileUploadModule, UploadEvent } from 'primeng/fileupload';
import { AutoCompleteCompleteEvent, AutoCompleteModule, AutoCompleteSelectEvent } from 'primeng/autocomplete';
import { UsuarioService } from '@services/usuario.service';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';
import { IUserInfo } from '@domain/user.model';
import { AccountState } from '@state/account.state';
import { FormComponent } from './form.component';
import { FacultadService } from '@services/facultad.service';
import { IFacultadInfo } from '@domain/facultad.model';

@Component({
    selector: 'facultad-form',
    templateUrl: './facultad.form.html',
    standalone: true,
    imports: [
        CommonModule,
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

export class FacultadForm extends FormComponent<IFacultadInfo> implements OnInit {
    facultadService = inject(FacultadService)

    submitForm() {
        console.log(this.formGroup.value);
        switch (this.formMode) {
            case 0:
                if (!this.formGroup.valid) return;
                this.facultadService.add(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result); 
                    },
                });
                break;
            case 1:
                if (!this.formGroup.valid) return;
                this.facultadService.update(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result);
                    },
                });            
                break;
            case 2:
                if (this.selectedObject?.id==null) return;
                this.facultadService.delete(this.selectedObject?.id!)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result);
                    },
                });            
                break;
        }
    }

    onInit() {
        this.formGroup.addControl('nombre', new FormControl('', [Validators.required]));
    }
}
