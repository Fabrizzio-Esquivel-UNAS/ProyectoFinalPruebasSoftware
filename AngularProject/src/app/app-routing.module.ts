import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate, mapToCanActivateChild } from '@angular/router';
import { AuthGuard, requireAnyRole } from './core/auth.guard';

import { AppLayoutComponent } from './layout/app.layout.component';
import { LoginComponent } from './routes/auth/login/login.component';
import { SignupComponent } from './routes/auth/signup/signup.component';
import { HomeComponent } from './routes/home/home.component';
import { SolicitarComponent } from './routes/solicitar/solicitar.component';
import { LoginResolve } from './core/login.resolve';
import { LogoutComponent } from './routes/auth/logout/logout.component';
import { UsuariosComponent } from './routes/usuarios/usuarios.component';
import { EscuelasComponent } from './routes/escuelas/escuelas.component';
import { FacultadesComponent } from './routes/facultades/facultades.component';
import { LineasInvestigacionComponent } from './routes/lineasInvestigacion/lineasInvestigacion.component';
import { GruposInvestigacionComponent } from './routes/gruposInvestigacion/gruposInvestigacion.component';
import { CalendarioAsesoradoComponent } from './routes/calendario/calendarioAsesorado/calendarioAsesorado.component';
import { CalendarioAsesorComponent } from './routes/calendario/calendarioAsesor/calendarioAsesor.component';
import { CalendlyComponent } from './routes/auth/calendly/calendly.component';
import { CitasComponent } from './routes/citas/citas.component';
import { SolicitudesComponent } from './routes/solicitudes/solicitudes.component';
import { UserRole } from '@domain/user.model';
import { ReporteCitasEstadoComponent } from './routes/reporte-citas-estado/reporte-citas-estado.component';

const routes: Routes = [
    {
        path: '', component: AppLayoutComponent,
        children: [
            { path: '', component: HomeComponent }
        ]
    },
    {
        path: 'login',
        component: LoginComponent,
        resolve: { ready: LoginResolve },
    },
    {
        path: 'signup', component: SignupComponent
    },
    {
        path: 'logout', component: LogoutComponent
    },
    {
        path: 'signin-calendly', component: CalendlyComponent,
        canActivate: [requireAnyRole(UserRole.Asesor)],
    },    
    {
        path: 'solicitar', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.User)],
        children: [
            { path: '', component: SolicitarComponent }
        ]
    },
    {
        path: 'calendarioAsesorado', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Asesorado)],
        children: [
            { path: '', component: CalendarioAsesoradoComponent }
        ]
    },
    {
        path: 'calendarioAsesor', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Asesor)],
        children: [
            { path: '', component: CalendarioAsesorComponent }
        ]
    },    
    {
        path: 'citas', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Asesorado, UserRole.Asesor)],
        children: [
            { path: '', component: CitasComponent }
        ]
    },
    {
        path: 'solicitudes', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.User, UserRole.Asesorado, UserRole.Asesor)],
        children: [
            { path: '', component: SolicitudesComponent }
        ]
    },
    {
        path: 'usuarios', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Coordinador, UserRole.Admin)],
        children: [
            { path: '', component: UsuariosComponent }
        ]
    },
    {
        path: 'escuelas', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Admin)],
        children: [
            { path: '', component: EscuelasComponent }
        ]
    },
    {
        path: 'facultades', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Admin)],
        children: [
            { path: '', component: FacultadesComponent }
        ]
    },
    {
        path: 'gruposInvestigacion', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Admin)],
        children: [
            { path: '', component: GruposInvestigacionComponent }
        ]
    },
    {
        path: 'lineasInvestigacion', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Admin)],
        children: [
            { path: '', component: LineasInvestigacionComponent }
        ]
    },
    {
        path: 'reporte-citas-estado', component: AppLayoutComponent,
        canActivate: [requireAnyRole(UserRole.Coordinador, UserRole.Admin)],
        children: [
            { path: '', component: ReporteCitasEstadoComponent }
        ]
    },    
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
