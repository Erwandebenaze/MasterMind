import Html exposing (Html, button, div, input, text)
import Html.Attributes exposing (..)
import Html.Events exposing (onInput, onClick)
import String
import Http
-- Get sur /login 
-- ?name='nomDuJoueur'

main =
  Html.program
    { model = model
    , view = view
    , update = update
    , subscriptions = subscriptions
    }



-- MODEL


type alias Model =
  { playerName : String
  }


model : Model
model =
  Model ""



-- UPDATE


type Msg
  = Change String
  | Send (Result Http.Error String)

update : Msg -> Model -> Model
update msg model =
  case msg of
    Change newContent ->
      { model | playerName = newContent }

    Send (Ok name) ->
      ( { model | playerName = name }, Cmd.none)

    Send (Err _) ->
      (model, Cmd.none)




-- VIEW


view : Model -> Html Msg
view model =
  div []
    [ input [ placeholder "Nom du joueur", onInput Change ] []
    , button [ onClick Send ] [text "Envoyer"]
    ]

-- SUBSCRIPTIONS


subscriptions : Model -> Sub Msg
subscriptions model =
  Sub.none



-- HTTP


getRandomGif : String -> Cmd Msg
getRandomGif topic =
  let
    url =
      "https://api.giphy.com/v1/gifs/random?api_key=dc6zaTOxFJmzC&tag=" ++ topic
  in
    Http.send Send (model | playerName)

