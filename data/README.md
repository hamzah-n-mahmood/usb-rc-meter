# The data

Every CSV here is the data behind one episode. The first row names the columns. Units are in the column names (ohm, F, V, A, pct = percent). The same numbers are on the companion dashboard.

## `EP01/thesis_specification_claims.csv`
What the 2014 thesis claimed, the evidence it gave, and the status after the review in this series.

Columns: item, thesis_claim_2014, evidence_in_thesis, status_after_review, episode

## `EP02/exp1_constant_current_stages.csv`
Experiment 1: the voltage across each test resistor on each current stage, and the current that implies.

Columns: stage, R_DUT_ohm (test resistor), V_RDUT_V (volts across it), I_C (current), I_unit

## `EP03/stage_resolution_table.csv`
Each current stage's current, the resistance one ADC count stands for, and the largest resistance it can read.

Columns: stage, current_A, ohms_per_count, max_R_ohm (1023 counts)

## `EP04/exp4_proteus_capacitance.csv`
Experiment 4 (simulated in Proteus): capacitance readings against the rated value, and the extra timer counts.

Columns: rated_F, simulated_reading_F, error_pct, extra_timer_counts

## `EP05/protocol_examples.csv`
Example text frames sent from the PIC to the PC, how each is decoded, and what the 2014 app showed.

Columns: frame, type, field_1, field_2, formula, GUI_display_2014, note

## `EP06/exp6_resistance.csv`
Experiment 6: 27 resistors measured once each on my meter, the SANWA CD800a and the TE-832B.

Columns: rated_ohm, tolerance_pct, SANWA_CD800a_ohm, TE832B_ohm, RC_meter_ohm, err_vs_rated_pct, err_vs_CD800a_pct, CD800a_spec_uncertainty_pct, note

## `EP06/exp6_capacitance.csv`
Experiment 6: 10 capacitors measured once each on my meter and the SANWA CD800a.

Columns: rated_F, tolerance_pct, SANWA_CD800a_F, RC_meter_F, err_vs_rated_pct, err_vs_SANWA_pct, within_tolerance
