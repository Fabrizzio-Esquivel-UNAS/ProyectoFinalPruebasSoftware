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
import { IEscuelaInfo } from '@domain/escuela.model';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { EscuelaForm } from '@ui/escuela.form';
import { EscuelaService } from '@services/escuela.service';
import { EscuelaState } from '@state/escuela.state';

@Component({
  selector: 'app-escuelas',
  templateUrl: './escuelas.component.html',
  styleUrl: './escuelas.component.css',
  standalone: true,
  imports: [
    TranslocoModule,
    ButtonModule,
    ReactiveFormsModule,
    EscuelaForm,
    DialogModule,
    RippleModule,
    InputTextModule,
    TableModule,
    AutoCompleteModule,
    StyleClassModule,
    InputMaskModule,
]
})
export class EscuelasComponent implements OnInit {
    http = inject(HttpClient);
    escuelaState = inject(EscuelaState);
    public selectedEscuela: any | undefined;
    public escuelas: IEscuelaInfo[] = [];
    formularios: any = {};

    ngOnInit() {
        this.escuelaState.stateItem$.subscribe(escuelas => {
            if (escuelas) {
                this.escuelas = escuelas;
            }
        })
    }
}
