import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { TableModule, TableRowSelectEvent, TableRowUnSelectEvent } from 'primeng/table';
import { TranslocoModule } from '@jsverse/transloco';
import { IUserInfo } from '@domain/user.model';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { UsuarioService } from '@services/usuario.service';
import { IFacultadInfo } from '@domain/facultad.model';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { FacultadForm } from '@ui/facultad.form';
import { FacultadService } from '@services/facultad.service';
import { FacultadState } from '@state/facultad.state';

@Component({
  selector: 'app-facultades',
  templateUrl: './facultades.component.html',
  styleUrl: './facultades.component.css',
  standalone: true,
  imports: [
    TranslocoModule,
    ButtonModule,
    ReactiveFormsModule,
    FacultadForm,
    DialogModule,
    RippleModule,
    InputTextModule,
    TableModule,
    AutoCompleteModule,
    StyleClassModule,
    InputMaskModule,
]
})
export class FacultadesComponent implements OnInit {
    http = inject(HttpClient);
    facultadState = inject(FacultadState);
    public selectedFacultad: any | undefined;
    public facultades: IFacultadInfo[] = [];
    formularios: any = {};

    ngOnInit() {
        this.facultadState.stateItem$.subscribe(facultades => {
            if (facultades) {
                this.facultades = facultades;
            }
        })
    }
}