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
import { catchError, filter, first, take, throwError } from 'rxjs';
import { InputNumberModule } from 'primeng/inputnumber';
import { FileUploadEvent, FileUploadModule, UploadEvent } from 'primeng/fileupload';
import { AutoCompleteCompleteEvent, AutoCompleteModule, AutoCompleteSelectEvent } from 'primeng/autocomplete';
import { IEscuelaInfo } from '@domain/escuela.model';
import { UsuarioService } from '@services/usuario.service';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';
import { IUserInfo } from '@domain/user.model';
import { EscuelaService } from '@services/escuela.service';
import { AccountState } from '@state/account.state';
import { FormComponent } from './form.component';
import { FacultadService } from '@services/facultad.service';
import { IFacultadInfo } from '@domain/facultad.model';
import { FacultadState } from '@state/facultad.state';

@Component({
    selector: 'escuela-form',
    templateUrl: './escuela.form.html',
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

export class EscuelaForm extends FormComponent<IEscuelaInfo> implements OnInit {
    escuelaService = inject(EscuelaService);
    facultadService = inject(FacultadService);
    facultadState = inject(FacultadState);
    filteredFacultades: IFacultadInfo[] = [];

    submitForm() {
        switch (this.formMode) {
            case 0:
                if (!this.formGroup.valid) return;
                this.escuelaService.add(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result);
                    },
                });
                break;
            case 1:
                if (!this.formGroup.valid) return;
                this.escuelaService.update(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result);
                    },
                });            
                break;
            case 2:
                if (this.selectedObject?.id==null) return;
                this.escuelaService.delete(this.selectedObject?.id!)
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
        this.formGroup.addControl('facultadId', new FormControl('', [Validators.required]));

        this.facultadState.stateItem$.pipe(
            // https://stackoverflow.com/questions/62584424/get-the-first-item-that-matches-a-condition-in-observable
            // https://stackoverflow.com/questions/42345969/take1-vs-first
            filter(x => x!=null),
            take(1)
        )
        .subscribe(facultades => {
            if (facultades) {
                this.filteredFacultades = facultades;                
            }
        });        
    }
}
