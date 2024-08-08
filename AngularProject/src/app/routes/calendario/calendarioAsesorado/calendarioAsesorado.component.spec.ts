import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarioAsesoradoComponent } from './calendarioAsesorado.component';

describe('CalendarioAsesoradoComponent', () => {
  let component: CalendarioAsesoradoComponent;
  let fixture: ComponentFixture<CalendarioAsesoradoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CalendarioAsesoradoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CalendarioAsesoradoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
