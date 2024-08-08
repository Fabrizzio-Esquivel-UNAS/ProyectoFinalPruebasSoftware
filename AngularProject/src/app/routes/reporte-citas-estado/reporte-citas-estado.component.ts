import { Component, NgModule, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Table, TableModule, TableRowSelectEvent, TableRowUnSelectEvent } from 'primeng/table';
import { TranslocoModule } from '@jsverse/transloco';
import { IUserInfo } from '@domain/user.model';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { UsuarioService } from '@services/usuario.service';
import { ICitaInfo } from '@domain/cita.model';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { CitaForm } from '@ui/cita.form';
import { CitaService } from '@services/cita.service';
import { CitaState } from '@state/cita.state';
import { CalendarioService } from '@services/calendario.service';
import { Observable } from 'rxjs';
import { DropdownModule } from 'primeng/dropdown';
import { TagModule } from 'primeng/tag';



@Component({
  selector: 'app-reporte-citas-estado',
  templateUrl: './reporte-citas-estado.component.html',
  styleUrl: './reporte-citas-estado.component.css',
  standalone: true,
  imports: [
    TranslocoModule,
    ButtonModule,
    ReactiveFormsModule,
    CitaForm,
    DialogModule,
    RippleModule,
    InputTextModule,
    TableModule,
    AutoCompleteModule,
    StyleClassModule,
    InputMaskModule,
    DropdownModule,
    TagModule,
  ]
})
export class ReporteCitasEstadoComponent {
  http = inject(HttpClient);
  citaState = inject(CitaState);
  calendarioService = inject(CalendarioService);
  public selectedCita: any | undefined;
  public citas: ICitaInfo[] = [];
  formularios: any = {};
  public citasFiltradas: ICitaInfo[] = [];
  searchValue: string | undefined;
  loading: boolean = false;
  statuses!: any[];
  
  onRefresh() {
      this.calendarioService.sync("");
  }

  ngOnInit() {
      this.citaState.stateItem$.subscribe(citas => {
          if (citas) {
              this.citas = citas;
          }
      })

      this.statuses = [
        { label: 'Programado', value: '0' },
        { label: 'Completado', value: '1' },
        { label: 'Inasistido', value: '2' },
        { label: 'Justificado', value: '3' },
        { label: 'Cancelado', value: '4' }
      ];
  }

  clear(table: Table) {
    table.clear();
    this.searchValue = ''
  }


}
