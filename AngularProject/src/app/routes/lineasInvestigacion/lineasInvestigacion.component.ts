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
import { ILineaInvestigacionInfo } from '@domain/lineaInvestigacion.model';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { LineaInvestigacionForm } from '@ui/lineaInvestigacion.form';
import { LineaInvestigacionService } from '@services/lineaInvestigacion.service';
import { LineaInvestigacionState } from '@state/lineaInvestigacion.state';

@Component({
  selector: 'app-lineasinvestigacion',
  templateUrl: './lineasInvestigacion.component.html',
  styleUrl: './lineasInvestigacion.component.css',
  standalone: true,
  imports: [
    TranslocoModule,
    ButtonModule,
    ReactiveFormsModule,
    LineaInvestigacionForm,
    DialogModule,
    RippleModule,
    InputTextModule,
    TableModule,
    AutoCompleteModule,
    StyleClassModule,
    InputMaskModule,
]
})
export class LineasInvestigacionComponent implements OnInit {
    http = inject(HttpClient);
    lineainvestigacionState = inject(LineaInvestigacionState);
    public selectedLineaInvestigacion: any | undefined;
    public lineasInvestigacion: ILineaInvestigacionInfo[] = [];
    formularios: any = {};

    ngOnInit() {
        this.lineainvestigacionState.stateItem$.subscribe(lineasInvestigacion => {
            if (lineasInvestigacion) {
                this.lineasInvestigacion = lineasInvestigacion;
            }
        })
    }
}