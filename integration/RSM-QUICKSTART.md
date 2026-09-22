# RSM Quick Start

Practical commands for the RSM 3.2.0 baseline used in this project.

## 1. Install the generated script

Build RSM with:

    integration/tools/copy-rsm.sh

The command builds the project and copies the generated Programmable Block
script to the system clipboard.

Paste that script into the ship's RSM Programmable Block and compile.

## 2. Initial ship setup

After RSM has booted, initialise the ship with:

    init:SHIPNAME

Example:

    init:CORVETTE

The default init performs the normal RSM initialisation, including block-name
initialisation, subsystem values and item/inventory values.

Optional switches disable individual parts:

    init:SHIPNAME:nonames
    init:SHIPNAME:nosubs
    init:SHIPNAME:noinv

They can also be combined:

    init:SHIPNAME:nonames:nosubs:noinv

Meaning:

    nonames = do not initialise RSM block names
    nosubs  = do not initialise subsystem values
    noinv   = do not initialise item/inventory values

IMPORTANT:
RSM writes generated configuration into the Programmable Block Custom Data
during its initialisation process. Back up any existing Custom Data you want
to preserve before running init:.

## 3. Stances

Set a stance with:

    stance:NAME

Examples:

    stance:Cruise
    stance:Combat
    stance:Docked
    stance:Docking
    stance:CQB
    stance:WeaponsHot

The available stance names depend on the configured RSM stance table.

## 4. HUD LCD command

RSM exposes:

    hudlcd:NAME

and an optional configuration argument:

    hudlcd:NAME:CONFIG

The normal RSM HUD LCD setup uses six LCDs named:

    ..[RSM].HUD1
    ..[RSM].HUD2
    ..[RSM].HUD3
    ..[RSM].HUD4
    ..[RSM].HUD5
    ..[RSM].HUD6

The separate HudLcd plugin configuration may also be present on those LCDs.

## 5. Useful diagnostics / utilities

Print detected block IDs:

    printblockids

Print properties for a named block/group:

    printblockprops:NAME

Spawn handling:

    spawn:open
    spawn:close

Projector position handling:

    projectors:save
    projectors:load

## 6. Configuration changes

RSM parses its Custom Data configuration on recompile. After changing
configuration, recompile the Programmable Block before expecting the changed
settings to take effect.

## 7. First baseline test

For the first stock-RSM test:

    1. Back up the current RSM PB Custom Data.
    2. Paste the generated baseline script.
    3. Compile.
    4. Allow RSM to finish booting.
    5. Run init:SHIPNAME.
    6. Inspect the automatic block naming.
    7. Inspect the six HUD LCDs.
    8. Try a few stance changes.
    9. Record what is useful, missing, or visually unpleasant.

Do not modify the RSM source during this baseline test.
