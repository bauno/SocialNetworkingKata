// Define your library scripting code here

type Post = {
    User: string
    Content: string
    TimeStamp: System.DateTime
}

type Command =
    | Post of string*string
    | Wall of string
    | Invalid of string


let parse (cmdStr:string) =
    Post("pippo","pluto")

let post name message =
    printfn "%s says: %s" name message
    {User="Pippo"; Content = "pluto"; TimeStamp = System.DateTime.Now}

let exec = 
    function
    | Post (name,message) -> post name message |> Some
    | _ -> None


let save post =
    printfn "The post of %s containing %s is saved" post.User post.Content
    post |> Some

    

let bind f x = 
    match x with
    |Some x -> f x
    |None -> x



let rop = parse >> exec >> bind save

let res = "Bauno" |> parse |> exec |> bind save

let res2 = "Bauno" |> rop
