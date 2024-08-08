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
import { IEscuelaInfo } from '@domain/escuela.model';
import { UsuarioService } from '@services/usuario.service';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';
import { IUserInfo } from '@domain/user.model';
import { EscuelaService } from '@services/escuela.service';
import { AccountState } from '@state/account.state';

@Component({
    template: '',
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

export abstract class FormComponent<T> implements OnChanges, OnInit {
    authService = inject(AuthService);
    accountState = inject(AccountState);
    translocoService = inject(TranslocoService);
    formModes = {
        add: 0,
        edit: 1,
        delete: 2        
    }

    @Input() formMode = this.formModes.add; // 0=add; 1=edit; 2=delete
    @Output() submitEvent = new EventEmitter<any>();
    @Input() selectedObject: T | undefined;
    @Input() visible: boolean | undefined;
    @Output() visibleChange: EventEmitter<boolean> = new EventEmitter<boolean>();
    formGroup: FormGroup = new FormGroup({});

    abstract onInit(): void
    ngOnInit(): void {
        if (this.formMode==this.formModes.edit){
            this.formGroup.addControl('id', new FormControl(Validators.required))
        }
        this.onInit();  
    }
    // Asignar los valores del selectedObject en el formGroup dinamicamente
    ngOnChanges(changes: SimpleChanges): void {
        if (changes['selectedObject']==null || this.selectedObject==null) return;
        for (const key in this.formGroup.controls) {
            const formVal = this.formGroup.controls[key];
            const objectVal = this.selectedObject[key as keyof typeof this.selectedObject];
            if (objectVal!=null) {
                formVal.setValue(objectVal);
                if (this.formMode==this.formModes.edit){
                    formVal.enable();
                } else if(this.formMode==this.formModes.delete) {
                    formVal.disable();
                }
            }else{
                formVal.setValue(null);
            }
        }
    }
}
