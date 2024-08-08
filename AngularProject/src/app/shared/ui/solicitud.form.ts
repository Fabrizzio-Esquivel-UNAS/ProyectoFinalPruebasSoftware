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
import { catchError, filter, take, throwError } from 'rxjs';
import { InputNumberModule } from 'primeng/inputnumber';
import { FileUploadEvent, FileUploadModule, UploadEvent } from 'primeng/fileupload';
import { AutoCompleteCompleteEvent, AutoCompleteModule, AutoCompleteSelectEvent } from 'primeng/autocomplete';
import { UsuarioService } from '@services/usuario.service';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';
import { IUserInfo } from '@domain/user.model';
import { AccountState } from '@state/account.state';
import { FormComponent } from './form.component';
import { SolicitudService } from '@services/solicitud.service';
import { ISolicitudInfo, SolicitudStatus } from '@domain/solicitud.model';
import { CitaEstado } from '@domain/cita.model';
import { UsuarioState } from '@state/usuario.state';
import { InputTextareaModule } from 'primeng/inputtextarea';

@Component({
    selector: 'solicitud-form',
    templateUrl: './solicitud.form.html',
    standalone: true,
    imports: [
        CommonModule,
        TranslocoModule,
        ReactiveFormsModule,
        RouterModule,
        InputTextareaModule,
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

export class SolicitudForm extends FormComponent<ISolicitudInfo> implements OnInit {
    usuarioState = inject(UsuarioState)
    usuarioService = inject(UsuarioService)
    solicitudService = inject(SolicitudService)
    filteredUsuarios: IUserInfo[] = [];
    estados: any[] = [];

    submitForm() {
    }

    onInit() {
        this.formGroup.addControl('estado', new FormControl('', [Validators.required]));
        this.formGroup.addControl('mensaje', new FormControl('', [Validators.required]));
        this.formGroup.addControl('asesoradoUserId', new FormControl('', [Validators.required]));
        this.formGroup.addControl('asesorUserId', new FormControl('', [Validators.required]));

        this.usuarioState.stateItem$.pipe(
            // https://stackoverflow.com/questions/62584424/get-the-first-item-that-matches-a-condition-in-observable
            // https://stackoverflow.com/questions/42345969/take1-vs-first
            filter(x => x!=null),
            take(1)
        )
        .subscribe(usuarios => {
            if (usuarios) {
                this.filteredUsuarios = usuarios;                
            }
        });     

        this.translocoService.selectTranslation().subscribe(itemLabels => {
            this.estados = [];
            const stringKeys = Object
                .keys(SolicitudStatus)
                .filter((v) => isNaN(Number(v)))
    
            stringKeys.forEach((key, index) => {
                this.estados.push({ label: itemLabels['solicitudStatus.'+index], value: index});
            })        
        })        
    }
}
