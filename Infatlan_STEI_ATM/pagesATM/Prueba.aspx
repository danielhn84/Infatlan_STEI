<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="Prueba.aspx.cs" Inherits="Infatlan_STEI_ATM.pagesATM.Prueba" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
     <title>Smart Wizard - a JavaScript jQuery Step Wizard plugin</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Include Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">

    <!-- Include SmartWizard CSS -->
    <link href="../dist/css/smart_wizard.css" rel="stylesheet" type="text/css" />

    <!-- Optional SmartWizard theme -->
    <link href="../dist/css/smart_wizard_theme_circles.css" rel="stylesheet" type="text/css" />
    <link href="../dist/css/smart_wizard_theme_arrows.css" rel="stylesheet" type="text/css" />
    <link href="../dist/css/smart_wizard_theme_dots.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
        <div class="container">
        
        <!-- SmartWizard html -->
        <div id="smartwizard">
            <ul>
                <li><a href="#step-1">Pregunta 1<br /><small></small></a></li>
                <li><a href="#step-2">Pregunta 2<br /><small></small></a></li>
                <li><a href="#step-3">Pregunta 3<br /><small></small></a></li>
                <li><a href="#step-4">Pregunta 4<br /><small></small></a></li>
            </ul>

            <div>
                <div id="step-1" class="">
                    <h3 class="border-bottom border-gray pb-2">Pregunta 1</h3>
                <p>OPCIONES</p>
                </div>
                <div id="step-2" class="">
                    <h3 class="border-bottom border-gray pb-2">PREGUNTA 2</h3>
                    <p>RESPUESTA 2</p>
                </div>
                <div id="step-3" class="">
                   <p>PREGUNTA 3</p>
                    <p>RESPUESTA</p>
                </div>
                <div id="step-4" class="">
                    <h3 class="border-bottom border-gray pb-2">PREGUNTA 4</h3>
                   <p>RESPUESTA 4</p>
                </div>
            </div>
        </div>


    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
     <script
      src="https://code.jquery.com/jquery-3.3.1.min.js"
      integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
      crossorigin="anonymous"></script>

    <!-- <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script> -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>

    <!-- Include SmartWizard JavaScript source -->
    <script type="text/javascript" src="../dist/js/jquery.smartWizard.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function(){

            // Step show event
            $("#smartwizard").on("showStep", function(e, anchorObject, stepNumber, stepDirection, stepPosition) {
               //alert("You are on step "+stepNumber+" now");
               if(stepPosition === 'first'){
                   $("#prev-btn").addClass('disabled');
               }else if(stepPosition === 'final'){
                   $("#next-btn").addClass('disabled');
               }else{
                   $("#prev-btn").removeClass('disabled');
                   $("#next-btn").removeClass('disabled');
               }
            });

            $("#smartwizard").on("endReset", function() {
              $("#next-btn").removeClass('disabled');
            });

            // Toolbar extra buttons
            var btnFinish = $('<button></button>').text('')
                                             .addClass('btn btn-info')
                                             .on('click', function () { alert('Finish Clicked'); })
                                             .css("display", "none") ;
            var btnCancel = $('<button></button>').text('Reiniciar')
                                             .addClass('btn btn-danger')
                                             .on('click', function(){ $('#smartwizard').smartWizard("reset"); });


            // Smart Wizard
            $('#smartwizard').smartWizard({
                    selected: 0,
                    theme: 'default',
                    transitionEffect:'fade',
                    showStepURLhash: true,
                    toolbarSettings: {toolbarPosition: 'both',
                                      toolbarButtonPosition: 'end',
                                      toolbarExtraButtons: [btnFinish, btnCancel]
                                    }
            });


            // External Button Events
            $("#reset-btn").on("click", function() {
                // Reset wizard
                $('#smartwizard').smartWizard("reset");
                $("#next-btn").removeClass('disabled').text('Siguiente');
                return true;
            });

            $("#prev-btn").on("click", function() {
                // Navigate previous
                $('#smartwizard').smartWizard("prev");
                return true;
            });

            $("#next-btn").on("click", function() {
                // Navigate next
                $('#smartwizard').smartWizard("next");
                return true;
            });

            $("#theme_selector").on("change", function() {
                // Change theme
                $('#smartwizard').smartWizard("theme", $(this).val());
                return true;
            });

            // Set selected theme on page refresh
            $("#theme_selector").change();
        });
    </script>
</asp:Content>
