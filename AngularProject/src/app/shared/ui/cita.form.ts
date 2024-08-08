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
import { CitaEstado, ICitaInfo } from '@domain/cita.model';
import { UsuarioService } from '@services/usuario.service';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';
import { IUserInfo, UserRole } from '@domain/user.model';
import { CitaService } from '@services/cita.service';
import { AccountState } from '@state/account.state';
import { FormComponent } from './form.component';
import { FacultadService } from '@services/facultad.service';
import { IFacultadInfo } from '@domain/facultad.model';
import { FacultadState } from '@state/facultad.state';
import { UsuarioState } from '@state/usuario.state';
import { InputTextareaModule } from 'primeng/inputtextarea';

@Component({
    selector: 'cita-form',
    templateUrl: './cita.form.html',
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
        InputTextareaModule,
        FileUploadModule,
        RippleModule
    ]
})

export class CitaForm extends FormComponent<ICitaInfo> implements OnInit {
    usuarioState = inject(UsuarioState)
    usuarioService = inject(UsuarioService)    
    citaService = inject(CitaService);
    facultadService = inject(FacultadService);
    facultadState = inject(FacultadState);
    filteredFacultades: IFacultadInfo[] = [];
    estados: any[] = [];
    rol = UserRole.User;

    submitForm() {
        if (!this.formGroup.valid) return;
        console.log(this.formGroup.value);
        this.citaService.update(this.formGroup.value)
        .subscribe({
            next: (_result) => {
                this.visibleChange.emit(false);
                this.submitEvent.emit(_result);
            },
        });
    }

    onInit() {
        this.formGroup.addControl('estado', new FormControl('', [Validators.required]));
        this.formGroup.addControl('desarrolloAsesor', new FormControl('', []));
        this.formGroup.addControl('desarrolloAsesorado', new FormControl('', []));

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
        
        this.translocoService.selectTranslation().subscribe(itemLabels => {                
            this.estados = [];
            const stringKeys = Object
                .keys(CitaEstado)
                .filter((v) => isNaN(Number(v)))
    
            stringKeys.forEach((key, index) => {
                this.estados.push({ label: itemLabels['citaEstados.'+index], value: index});
            })        
        });

        this.accountState.stateItem$.pipe(
            filter(x => x!=null),
            take(1)
        ).subscribe(accountInfo => {
            this.rol = accountInfo?.role!;
        })
    }
}
